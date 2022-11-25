namespace Domain;

public partial class TblUserDetail
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? EmailId { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
