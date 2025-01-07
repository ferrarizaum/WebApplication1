using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(256)]
    public string Nome { get; set; }

    [Required]
    [StringLength(256)]
    public string Login { get; set; }

    [Required]
    [StringLength(256)]
    public string Email { get; set; }

    [Required]
    [StringLength(256)]
    public string Senha { get; set; }

    [Column(TypeName = "varchar(max)")]
    [DefaultValue("")]
    public string ChaveVerificacao { get; set; }

    [Column(TypeName = "varchar(max)")]
    public string LastToken { get; set; }

    [DefaultValue(false)]
    public bool IsVerificado { get; set; }

    [DefaultValue(true)]
    public bool Ativo { get; set; } = true;

    [DefaultValue(false)]
    public bool Excluido { get; set; } = false;
}
