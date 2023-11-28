using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet8NewFeatures.Models
{
    public class TreeNode
    {
        public TreeNode(int id, int? parentId, string name, HierarchyId path)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            Path = path;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public TreeNode? Parent { get; set; }
        public ICollection<TreeNode>? SubCategories { get; set; }
        public int? ParentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public HierarchyId Path { get; set; }
    }
}
