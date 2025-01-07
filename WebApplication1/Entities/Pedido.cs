using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UsuarioId { get; set; }

    [Required]
    public DateTime DataPedido { get; set; }
}
