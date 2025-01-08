using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// 1.E
public class PedidoItem
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Pedido")]
    public int PedidoId { get; set; }
    [ForeignKey("Produto")]
    public int ProdutoId { get; set; }

    public int Quantidade { get; set; }
}
