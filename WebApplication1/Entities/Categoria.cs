using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(max)")]
    public string Nome { get; set; }

    [Required]
    [Column(TypeName = "varchar(max)")]
    public string Url { get; set; }

    [DefaultValue(true)]
    public bool Ativo { get; set; }

    [DefaultValue(false)]
    public bool Excluido { get; set; }
}
