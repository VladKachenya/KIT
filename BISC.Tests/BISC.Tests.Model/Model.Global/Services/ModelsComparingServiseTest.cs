using BISC.Model.Global.Factorys;
using BISC.Model.Global.Model;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Global.Project;
using BISC.Model.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Factorys;
using BISC.Modules.DataSets.Model.Model;
using BISC.Modules.Device.Model.Model;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Modules.InformationModel.Model.DataTypeTemplates;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Model.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BISC.Modules.Connection.MMS.MMS_ASN1_Model.Operator_Station_instance.DefinitionChoiceType.DetailsSequenceType.StationTypeEnumType;

namespace BISC.Tests.Model.Model.Global.Services
{
    [TestClass]
    public class ModelsComparingServiseTest
    {
        ModelsComparingServise modelsComparingServise;
        [TestInitialize]
        public void TestInitialize()
        {
            modelsComparingServise = new ModelsComparingServise(new TestMismuchFactory()); 
        }

        #region Tests

        [TestMethod]
        public void CompareBranches_TestBransh1CompareTestBransh1_NullListOfIMismatch()
        {
            IModelElement testBransh1 = TestBransh1();
            IModelElement testBransh2 = TestBransh1();
            var result = modelsComparingServise.CompareBranches(testBransh1, testBransh2);
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void CompareBranches_TestBransh2CompareTestBransh3_9ValuesOfMissingMismatch()
        {
            IModelElement testBransh1 = TestBransh2();
            IModelElement testBransh2 = TestBransh3();
            var result = modelsComparingServise.CompareBranches(testBransh1, testBransh2);
            Assert.AreEqual(result.Count, 9);
            foreach (var element in result)
                Assert.AreEqual(element.MismatchType, "MissingMismatch", "Mismatch type does not match MissingMismatch");
        }

        [TestMethod]
        public void CompareBranches_TestBransh1CompareTestBransh1Mod_3ValuesOfInequalityMismatch()
        {
            IModelElement testBransh1 = new ModelElement() { ElementName = "T1", Namespace = "T2" };
            IModelElement testBransh2 = new ModelElement() { ElementName = "T1", Namespace = "T2" };
            testBransh1.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh1.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh1.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh1.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh1.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh1.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh2.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T1" });
            testBransh2.ChildModelElements.Add(new ModelElement() { ElementName = "T2", Namespace = "T2" });
            testBransh2.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh2.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            testBransh2.ChildModelElements.Add(new ModelElement() { ElementName = "T2", Namespace = "T2" });
            testBransh2.ChildModelElements.Add(new ModelElement() { ElementName = "T1", Namespace = "T2" });
            var result = modelsComparingServise.CompareBranches(testBransh1, testBransh2);
            Assert.AreEqual(result.Count, 3);
            foreach (var element in result)
                Assert.AreEqual(element.MismatchType, "InequalityMismatch", "Mismatch type does not match InequalityMismatch");
        }


        #endregion

        #region Servise

        private IModelElement TestBransh1()
        {
            IModelElement result = new ModelElement() { ElementName = "ModelElementElementName", Namespace = "ModelElementNamespace" };
            BISC_Model_Global(result);
            BISC_Modules_Device_Model(result.ChildModelElements[0]);
            BISC_Modules_Gooses_Model(result.ChildModelElements[1]);
            BISC_Modules_InformationModel_Model_Path1(result.ChildModelElements[2]);
            BISC_Modules_DataSets_Model(result.ChildModelElements[3]);
            BISC_Modules_InformationModel_Model_Path2(result.ChildModelElements[2].ChildModelElements[1]);
            return result;
        }

        private IModelElement TestBransh2()
        {
            IModelElement result = new ModelElement() { ElementName = "ModelElementElementName", Namespace = "ModelElementNamespace" };
            BISC_Modules_InformationModel_Model_Path1(result);
            BISC_Modules_InformationModel_Model_Path1(result.ChildModelElements[0]);
            return result;
        }

        private IModelElement TestBransh3()
        {
            IModelElement result = new ModelElement() { ElementName = "ModelElementElementName", Namespace = "ModelElementNamespace" };
            BISC_Modules_InformationModel_Model_Path1(result);
            return result;
        }

        private void BISC_Model_Global(IModelElement element)
        {
            IModelElement[] childrens =
                {
                    new SclCommunicationModel{ ElementName = "SclCommunicationModelElementName", Namespace = "SclCommunicationModelNamespace"},
                    new SubNetwork{ ElementName = "SubNetworkElementName", Namespace = "SubNetworkNamespace", Name = "Name1", Desc = "SubNetworkDesc", Type = "SubNetworkType" },
                    new DurationInMilliSec{ ElementName = "DurationInMilliSecElementName", Namespace = "DurationInMilliSecNamespace", Unit = "DurationInMilliSec"},
                    new ConnectedAccessPoint{ElementName = "ConnectedAccessPointElementName", Namespace = "ConnectedAccessPointNamespace", IedName = "ConnectedAccessPointIedName", ApName = "ConnectedAccessPointApName"},
                    //new Gse{ ElementName = "GseElementName", Namespace = "GseNamespace", LdInst = "GseMacAddress", CbName = "GseMacAddress"},
                    new AddressProperty{ ElementName = "AddressProperty", Namespace = "AddressProperty", Type = "AddressPropertyType", Value ="AddressPropertyValue"},
                    new BiscProject{ ElementName = "BiscProjectElementName", Namespace = "BiscProjectNamespace"},
                    new SclAddress() { ElementName = "SclAddressElementName", Namespace = "SclAddressElementName" },
                    new SclModel() { ElementName = "SclModelElementName", Namespace = "SclModelNamespace" },
                    new ModelElement() { ElementName = "ModelElementElementName", Namespace = "ModelElementNamespace" }
                };
            element.ChildModelElements.AddRange(childrens);
        }

        private void BISC_Modules_DataSets_Model(IModelElement element)
        {
            IModelElement[] childrens =
                {
                    new DataSet() { ElementName = "DataSetElementName", Namespace = "DataSetNamespace", Name ="DataSetName", IsDynamic= true },
                    new Fcda() { ElementName = "FcdaElementName", Namespace = "FcdaElementNamespace", LdInst = "FcdaLdInst", Prefix = "FcdaPrefix", LnClass = "FcdaLnClass", LnInst = "FcdaLnInst", DoName = "FcdaDoName", DaName = "FcdaDaName" }
                };
            element.ChildModelElements.AddRange(childrens);
        }
        private void BISC_Modules_Device_Model(IModelElement element)
        {
            IModelElement[] childrens =
                {
                    new Device() { ElementName = "DeviceElementName", Namespace = "DeviceNamespace", Name = "DeviceGuid", Ip = "DeviceIp", Description = "DeviceDescription", Manufacturer = "DeviceManufacturer", Type = "DeviceType", Revision = "DeviceRevision" },
                };
            element.ChildModelElements.AddRange(childrens);
        }

        private void BISC_Modules_Gooses_Model(IModelElement element)
        {
            IModelElement[] childrens =
                {
                    new GooseRow() { ElementName = "GooseRowElementName", Namespace = "GooseRowNamespace", Signature = "GooseRowSignature", ReferencePath = "GooseRowReferencePath",
                        GooseRowType = "GooseRowGooseRowType", NumberOfFcdaInDataSetOfGoose = 5, ValueList = new List<bool>() { true, false, true, false} },
                    new SubscriberDevice() { ElementName = "SubscriberDeviceElementName", Namespace = "SubscriberDeviceNamespace", LdInst = "SubscriberDeviceLdInst",
                        ApRef = "SubscriberDeviceApRef", LnClass = "SubscriberDeviceLnClass", DeviceName = "SubscriberDeviceDeviceName" },
                    new GooseMatrix() { ElementName = "GooseMatrixElementName", Namespace = "GooseMatrixNamespace", RelatedIedName = "GooseMatrixRelatedIedName" },
                    new ExternalGooseRef() { ElementName = "ExternalGooseRefElementName", Namespace = "ExternalGooseRefNamespace", LdInst = "ExternalGooseRefLdInst",
                        Prefix = "ExternalGooseRefPrefix", LnClass = "ExternalGooseRefLnClass", LnInst = "ExternalGooseRefLnInst", DoName = "ExternalGooseRefDoName",
                        DaName = "ExternalGooseRefDaName" },
                    new GooseInput() { ElementName = "GooseInputElementName", Namespace = "GooseInputNamespace" },
                    new GooseControl() { ElementName = "GooseControlElementName", Namespace = "GooseControlNamespace", Name = "GooseControlName", DataSet = "GooseControlDataSet", ConfRev = 2, AppId = "GooseControlAppId" }
                };
            element.ChildModelElements.AddRange(childrens);
        }

        private void BISC_Modules_InformationModel_Model_Path1(IModelElement element)
        {
            IModelElement[] childrens =
            {
                    new Val() { ElementName = "ValElementName", Namespace = "ValNamespace" , Value = "ValValue"},
                    new DoType() { ElementName = "DoTypeElementName", Namespace = "DoTypeNamespace", Id = "DoTypeId", Cdc = "DoTypeCdc" },
                    new Doi() { ElementName = "DoiElementName", Namespace = "DoiNamespace", Name = "DoiName", Description = "DoiDescription" },
                    new Da() { ElementName = "Da", Namespace = "Da", Name = "Da", BType = "Da", Type = "Da", Fc = "Da" },
                    new DaType() { ElementName = "DaTypeElementName", Namespace = "DaTypeElementName", Id = "DaTypeElementName" },
                    new DeviceServer() { ElementName = "DeviceServerElementName", Namespace = "DeviceServerNamespace" },
                    new LDevice() { ElementName = "LDeviceElementName", Namespace = "LDeviceNamespace", Inst = "LDeviceInst" },
                    new Dai() { ElementName = "DaiElementName", Namespace = "DaiNamespace", Name = "DaiName", Description = "DaiDescription" },
                    new Sdi() { ElementName = "SdiElementName", Namespace = "SdiNamespace", Name = "SdiName" }
            };
            element.ChildModelElements.AddRange(childrens);
        }

        private void BISC_Modules_InformationModel_Model_Path2(IModelElement element)
        {
            IModelElement[] childrens =
            {
                    new Sdo() { ElementName = "SdoElementName", Namespace = "SdoNamespace", Name = "SdoName", Type = "SdoType" },
                    new Do() { ElementName = "DoElementName", Namespace = "DoNamespace", Name = "DoName", Type = "DoType" },
                    new LogicalNode() { ElementName = "LogicalNodeElementName", Namespace = "LogicalNodeNamespace", LnClass = "LogicalNodeLnClass", Inst = "LogicalNodeInst",
                    LnType = "LogicalNodeLnType", Prefix = "LogicalNodePrefix"},
                    new Bda() { ElementName = "BdaElementName", Namespace = "BdaNamespace", Name = "BdaName", BType = "BdaBType", Type = "BdaType" },
                    new DeviceAccessPoint() { ElementName = "DeviceAccessPoint", Namespace = "DeviceAccessPoint", Name = "DeviceAccessPoint", Router = true,  Clock = false},
                    new EnumVal() { ElementName = "EnumValElementName", Namespace = "EnumValNamespace", Ord = 3, Value = "EnumValValue" },
                    new LNodeType() { ElementName = "LNodeTypeElementName", Namespace = "LNodeTypeNamespace", Id = "LNodeTypeId", LnClass= "LNodeTypeLnClass" },
                    new DataTypeTemplates() { ElementName = "DataTypeTemplatesElementName", Namespace = "DataTypeTemplatesNamespace" },
                    new LogicalNodeZero() { ElementName = "LogicalNodeZeroElementName", Namespace = "LogicalNodeZeroNamespace" }
            };
            element.ChildModelElements.AddRange(childrens);
        }
        #endregion
    }


    public class TestMismuchFactory : MismatchFactory
    {

    }
}
