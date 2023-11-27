using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIC.Data.ViewModels.Import
{
    public class MemberDataSheet
    {
        [Column("الرقم القومي")]
        public string NationalID { get; set; }
        [Column("البريد الالكتروني")]
        public string Email { get; set; }
        [Column("الاسم")]
        public string Name { get; set; }
        [Column("تاريخ الميلاد")]
        public DateTime DateOfBirth { get; set; }
        [Column("محافظة الميلاد")]
        public string BirthGovernorate { get; set; }
        [Column("محل الإقامة")]
        public string Address { get; set; }
        [Column("المنطقة")]
        public string Region { get; set; }
        [Column("محافظة السكن")]
        public string CurrentGovernorate { get; set; }
        [Column("الحالة الإجتماعية")]
        public string MaritalStatus { get; set; }
        [Column("قسم الشرطة")]
        public string PoliceDepartment { get; set; }
        [Column("تليفون منزل")]
        public string HomePhone { get; set; }
        [Column("تليفون محمول 1")]
        public string MobileNumber1 { get; set; }
        [Column("تليفون محمول 2")]
        public string MobileNumber2 { get; set; }
        [Column("تليفون محمول 3")]
        public string MobileNumber3 { get; set; }
        [Column("الدرجة")]
        public string Grade { get; set; }
        [Column("تاريخ الحصول على الدرجة الحالية")]
        public DateTime CurrentGradeDate { get; set; }
        [Column("النيابة التي يعمل بها")]
        public string CurrentProsecution { get; set; }
        [Column("الوظيفة")]
        public string Job { get; set; }
        [Column("تاريخ استلام العمل")]
        public DateTime ReceivingWorkDate { get; set; }
        [Column("تاريخ التعيين")]
        public DateTime HiringDate { get; set; }
        [Column("تاريخ الإحالة للمعاش")]
        public DateTime ReferralDate { get; set; }
        [Column("تاريخ نقله للنيابة")]
        public DateTime TransferProsecutionDate { get; set; }
        [Column("المؤهل الدراسي")]
        public string AcademicQualification { get; set; }
        [Column("التقدير")]
        public string AcademicQualificationAssessment { get; set; }
        [Column("تاريخ الحصول عليه")]
        public DateTime AcademicQualificationDate { get; set; }
        [Column("جامعة المؤهل الدراسي")]
        public string AcademicQualificationUnv { get; set; }
        [Column("مؤهل اعلى")]
        public string HigherQualification { get; set; }
        [Column("تقدير المؤهل الأعلى")]
        public string HigherQualificationAssessment { get; set; }
        [Column("تاريخ الحصول علىه")]
        public DateTime HigherQualificationDate { get; set; }

    }
}
