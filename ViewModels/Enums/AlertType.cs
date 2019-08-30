using System.ComponentModel.DataAnnotations;

namespace ViewModels.Enums
{
    public enum AlertType
    {
        [Display(Name = "alert-success")]
        Success = 0,
        [Display(Name = "alert-info")]
        Info = 1,
        [Display(Name = "alert-warning")]
        Warning = 2,
        [Display(Name = "alert-danger")]
        Danger = 3
    };
}
