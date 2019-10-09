﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.NotesService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="NotesData", Namespace="http://schemas.datacontract.org/2004/07/GeneralContract")]
    [System.SerializableAttribute()]
    public partial class NotesData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HeaderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Header {
            get {
                return this.HeaderField;
            }
            set {
                if ((object.ReferenceEquals(this.HeaderField, value) != true)) {
                    this.HeaderField = value;
                    this.RaisePropertyChanged("Header");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Time {
            get {
                return this.TimeField;
            }
            set {
                if ((this.TimeField.Equals(value) != true)) {
                    this.TimeField = value;
                    this.RaisePropertyChanged("Time");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NotesService.INoteServiceContract")]
    public interface INoteServiceContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/Add", ReplyAction="http://tempuri.org/INoteServiceContract/AddResponse")]
        void Add(Client.NotesService.NotesData nd);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/Add", ReplyAction="http://tempuri.org/INoteServiceContract/AddResponse")]
        System.Threading.Tasks.Task AddAsync(Client.NotesService.NotesData nd);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/GetById", ReplyAction="http://tempuri.org/INoteServiceContract/GetByIdResponse")]
        void GetById(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/GetById", ReplyAction="http://tempuri.org/INoteServiceContract/GetByIdResponse")]
        System.Threading.Tasks.Task GetByIdAsync(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/GetAll", ReplyAction="http://tempuri.org/INoteServiceContract/GetAllResponse")]
        Client.NotesService.NotesData[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/GetAll", ReplyAction="http://tempuri.org/INoteServiceContract/GetAllResponse")]
        System.Threading.Tasks.Task<Client.NotesService.NotesData[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/SearchBy", ReplyAction="http://tempuri.org/INoteServiceContract/SearchByResponse")]
        string SearchBy();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/SearchBy", ReplyAction="http://tempuri.org/INoteServiceContract/SearchByResponse")]
        System.Threading.Tasks.Task<string> SearchByAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/DeleteNote", ReplyAction="http://tempuri.org/INoteServiceContract/DeleteNoteResponse")]
        void DeleteNote(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/DeleteNote", ReplyAction="http://tempuri.org/INoteServiceContract/DeleteNoteResponse")]
        System.Threading.Tasks.Task DeleteNoteAsync(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/Edit", ReplyAction="http://tempuri.org/INoteServiceContract/EditResponse")]
        void Edit(Client.NotesService.NotesData nt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/Edit", ReplyAction="http://tempuri.org/INoteServiceContract/EditResponse")]
        System.Threading.Tasks.Task EditAsync(Client.NotesService.NotesData nt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/DeleteAll", ReplyAction="http://tempuri.org/INoteServiceContract/DeleteAllResponse")]
        void DeleteAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INoteServiceContract/DeleteAll", ReplyAction="http://tempuri.org/INoteServiceContract/DeleteAllResponse")]
        System.Threading.Tasks.Task DeleteAllAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INoteServiceContractChannel : Client.NotesService.INoteServiceContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NoteServiceContractClient : System.ServiceModel.ClientBase<Client.NotesService.INoteServiceContract>, Client.NotesService.INoteServiceContract {
        
        public NoteServiceContractClient() {
        }
        
        public NoteServiceContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NoteServiceContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NoteServiceContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NoteServiceContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Add(Client.NotesService.NotesData nd) {
            base.Channel.Add(nd);
        }
        
        public System.Threading.Tasks.Task AddAsync(Client.NotesService.NotesData nd) {
            return base.Channel.AddAsync(nd);
        }
        
        public void GetById(System.Guid id) {
            base.Channel.GetById(id);
        }
        
        public System.Threading.Tasks.Task GetByIdAsync(System.Guid id) {
            return base.Channel.GetByIdAsync(id);
        }
        
        public Client.NotesService.NotesData[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<Client.NotesService.NotesData[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public string SearchBy() {
            return base.Channel.SearchBy();
        }
        
        public System.Threading.Tasks.Task<string> SearchByAsync() {
            return base.Channel.SearchByAsync();
        }
        
        public void DeleteNote(System.Guid id) {
            base.Channel.DeleteNote(id);
        }
        
        public System.Threading.Tasks.Task DeleteNoteAsync(System.Guid id) {
            return base.Channel.DeleteNoteAsync(id);
        }
        
        public void Edit(Client.NotesService.NotesData nt) {
            base.Channel.Edit(nt);
        }
        
        public System.Threading.Tasks.Task EditAsync(Client.NotesService.NotesData nt) {
            return base.Channel.EditAsync(nt);
        }
        
        public void DeleteAll() {
            base.Channel.DeleteAll();
        }
        
        public System.Threading.Tasks.Task DeleteAllAsync() {
            return base.Channel.DeleteAllAsync();
        }
    }
}
