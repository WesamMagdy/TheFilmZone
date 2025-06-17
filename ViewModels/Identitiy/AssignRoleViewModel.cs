namespace FilmZone.ViewModels.Identitiy
{
    public class AssignRoleViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SelectedRole { get; set; }

        public List<SelectListItem> AvailableRoles { get; set; } = new();
    }
}
