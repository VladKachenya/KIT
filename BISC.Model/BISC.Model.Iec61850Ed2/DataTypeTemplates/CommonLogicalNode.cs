namespace BISC.Model.Iec61850Ed2.DataTypeTemplates
{
    public class CommonLogicalNode : tLNodeType
    {
        public CommonLogicalNode()
        {

        }


        public CDCTypesEd2.LPL NamPlt { get; set; }
        public CDCTypesEd2.ENS Beh { get; set; }
        public CDCTypesEd2.ENS Health { get; set; }
        public CDCTypesEd2.SPS Blk { get; set; }
        public CDCTypesEd2.ENC Mod { get; set; }

        public CDCTypesEd2.SPC CmdBlk { get; set; }
        public CDCTypesEd2.ORG InRef1 { get; set; }
        public CDCTypesEd2.ORG BlkRef1 { get; set; }

        public CDCTypesEd2.SPS ClcExp { get; set; }
        public CDCTypesEd2.SPC ClcStr { get; set; }


        public CDCTypesEd2.ENG ClcMth { get; set; }
        public CDCTypesEd2.ENG ClcMod { get; set; }
        public CDCTypesEd2.ENG ClcIntvTyp { get; set; }

        public CDCTypesEd2.ING ClcIntvPer { get; set; }
        public CDCTypesEd2.ENG ClcRfTyp { get; set; }
        public CDCTypesEd2.ING ClcRfPer { get; set; }



        public CDCTypesEd2.ORG ClcSrc { get; set; }
        public CDCTypesEd2.ING ClcNxTmms { get; set; }
        public CDCTypesEd2.ORG InSyn { get; set; }
    }
}