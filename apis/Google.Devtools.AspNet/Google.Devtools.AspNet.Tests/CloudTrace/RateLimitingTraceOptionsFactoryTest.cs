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

using Xunit;

namespace Google.Devtools.AspNet.Tests
{
    public class RateLimitingTraceOptionsFactoryTest
    {
       

        [Fact]
        public void CreateOptions_ShouldTrace()
        {
            RateLimiter rateLimiter = TraceUtils.GetRateLimiter(1001);
            RateLimitingTraceOptionsFactory factory = new RateLimitingTraceOptionsFactory(rateLimiter);
            Assert.True(factory.CreateOptions().ShouldTrace);
        }

        [Fact]
        public void CreateOptions_ShouldNotTrace()
        {
            RateLimiter rateLimiter = TraceUtils.GetRateLimiter(999);
            RateLimitingTraceOptionsFactory factory = new RateLimitingTraceOptionsFactory(rateLimiter);
            Assert.False(factory.CreateOptions().ShouldTrace);
        }
    }
}