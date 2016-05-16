using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.ViewModels
{
    public class TP_ListViewModel
    {
        //TP单号
        public string TP_NO { get; set; }
        //TP名称
        public string TP_Name { get; set; }
        //TP单据目前状态
        public TP_State State { get; set; }
        //申请人
        public string ApplyUser { get; set; }
        //申请日期
        public DateTime ApplyDate { get; set; }
        //审核人
        public string ApproveUser { get; set; }
        //是否锁定
        public bool IsLocked { get; set; }
        //TP状态枚举
       public enum TP_State { 
            Create=1,
            Submit=2,
            Approve=3
        }
    }


}