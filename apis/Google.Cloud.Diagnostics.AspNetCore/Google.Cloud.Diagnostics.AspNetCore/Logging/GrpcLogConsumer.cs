﻿// Copyright 2016 Google Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using Google.Cloud.Logging.V2;
using Google.Cloud.Diagnostics.Common;
using Google.Api.Gax;
using System.Linq;

namespace Google.Cloud.Diagnostics.AspNetCore
{
    /// <summary>
    /// An <see cref="IConsumer{T}"/> that will send received logs to the Stackdriver Logging API.
    /// </summary>
    internal sealed class GrpcLogConsumer : IConsumer<LogEntry>
    {
        /// <summary>An empty dictionary used for labels.</summary>
        private static readonly IDictionary<string, string> EmptyDictionary = new Dictionary<string, string>();

        private LoggingServiceV2Client _client;

        /// <param name="client">The logging client that will push logs to the Stackdriver Logging API.</param>
        public GrpcLogConsumer(LoggingServiceV2Client client)
        {
            _client = GaxPreconditions.CheckNotNull(client, nameof(client));
        }

        /// <inheritdoc />
        public void Receive(IEnumerable<LogEntry> logs)
        {
            LogEntry entry = logs.FirstOrDefault();
            if (entry == null)
            {
                return;
            }

            LogNameOneof oneof = LogNameOneof.Parse(entry.LogName, false);
            _client.WriteLogEntriesAsync(oneof, null, EmptyDictionary, logs);
        }
    }
}