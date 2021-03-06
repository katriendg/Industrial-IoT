// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.Modules.OpcUa.Twin.v2.Supervisor.StartStop {
    using Microsoft.Azure.IIoT.Modules.OpcUa.Twin.Tests;
    using Microsoft.Azure.IIoT.OpcUa.Registry.Models;
    using Microsoft.Azure.IIoT.OpcUa.Testing.Fixtures;
    using Microsoft.Azure.IIoT.OpcUa.Testing.Tests;
    using Microsoft.Azure.IIoT.OpcUa.Twin;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;
    using Autofac;
    using System;

    [Collection(WriteCollection.Name)]
    public class SupervisorValueCallArrayTests {

        public SupervisorValueCallArrayTests(TestServerFixture server) {
            _server = server;
        }

        private CallArrayMethodTests<EndpointRegistrationModel> GetTests(
            string deviceId, string moduleId, IContainer services) {
            return new CallArrayMethodTests<EndpointRegistrationModel>(
                () => services.Resolve<INodeServices<EndpointRegistrationModel>>(),
                new EndpointRegistrationModel {
                    Endpoint = new EndpointModel {
                        Url = $"opc.tcp://{Dns.GetHostName()}:{_server.Port}/UA/SampleServer",
                        Certificate = _server.Certificate?.RawData
                    },
                    Id = "testid",
                    SupervisorId = SupervisorModelEx.CreateSupervisorId(deviceId, moduleId)
                });
        }

        private readonly TestServerFixture _server;
        private readonly bool _runAll = Environment.GetEnvironmentVariable("TEST_ALL") != null;

        [SkippableFact]
        public async Task NodeMethodMetadataStaticArrayMethod1TestAsync() {
            // Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodMetadataStaticArrayMethod1TestAsync();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodMetadataStaticArrayMethod2TestAsync() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodMetadataStaticArrayMethod2TestAsync();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodMetadataStaticArrayMethod3TestAsync() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodMetadataStaticArrayMethod3TestAsync();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod1Test1Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod1Test1Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod1Test2Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod1Test2Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod1Test3Async() {
            //  Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod1Test3Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod1Test4Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod1Test4Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod1Test5Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod1Test5Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod2Test1Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod2Test1Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod2Test2Async() {
            // Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod2Test2Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod2Test3Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod2Test3Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod2Test4Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod2Test4Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod3Test1Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod3Test1Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod3Test2Async() {
            Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod3Test2Async();
                });
            }
        }

        [SkippableFact]
        public async Task NodeMethodCallStaticArrayMethod3Test3Async() {
            // Skip.IfNot(_runAll);
            using (var harness = new TwinModuleFixture()) {
                await harness.RunTestAsync(async (device, module, services) => {
                    await GetTests(device, module, services).NodeMethodCallStaticArrayMethod3Test3Async();
                });
            }
        }

    }
}
