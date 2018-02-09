﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SparkSDKTests.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ITestFixtureService")]
    public interface ITestFixtureService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestFixtureService/SendCommandMsg", ReplyAction="http://tempuri.org/ITestFixtureService/SendCommandMsgResponse")]
        void SendCommandMsg(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITestFixtureService/SendCommandMsg", ReplyAction="http://tempuri.org/ITestFixtureService/SendCommandMsgResponse")]
        System.Threading.Tasks.Task SendCommandMsgAsync(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITestFixtureServiceChannel : SparkSDKTests.ServiceReference.ITestFixtureService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TestFixtureServiceClient : System.ServiceModel.ClientBase<SparkSDKTests.ServiceReference.ITestFixtureService>, SparkSDKTests.ServiceReference.ITestFixtureService {
        
        public TestFixtureServiceClient() {
        }
        
        public TestFixtureServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TestFixtureServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestFixtureServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TestFixtureServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SendCommandMsg(string message) {
            base.Channel.SendCommandMsg(message);
        }
        
        public System.Threading.Tasks.Task SendCommandMsgAsync(string message) {
            return base.Channel.SendCommandMsgAsync(message);
        }
    }
}
