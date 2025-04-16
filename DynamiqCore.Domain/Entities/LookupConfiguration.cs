using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamiqCore.Domain.Entities;

public class LookupConfiguration
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LookupConfigurationId { get; set; }

    [Required, StringLength(500)]
    public string Key { get; set; }
    
    [Required, StringLength(500)]
    public string Value { get; set; }
    
    [Required, StringLength(1000)]
    public string Description { get; set; }
}