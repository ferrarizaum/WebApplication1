using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// 1.B
public class Produto
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Categoria")]
    public int CategoriaId { get; set; }

    [Required]
    [Column(TypeName = "varchar(max)")]
    public string Nome { get; set; }

    [Required]
    [Column(TypeName = "varchar(max)")]
    public string Url { get; set; }

    public int Quantidade { get; set; }

    [DefaultValue(true)]
    public bool Ativo { get; set; }

    [DefaultValue(false)]
    public bool Excluido { get; set; }
}
