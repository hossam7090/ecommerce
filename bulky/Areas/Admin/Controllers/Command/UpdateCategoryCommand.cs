using bulky.Models.Models;
using Bullky.DataAccess.Repository.IRepository;

namespace bulky.Areas.Admin.Controllers.Command
{
	public class UpdateCategoryCommand : ICommand
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly Category _category;

		public UpdateCategoryCommand(IUnitOfWork unitOfWork, Category category)
		{
			_unitOfWork = unitOfWork;
			_category = category;
		}

		public void Execute()
		{
			_unitOfWork.Category.Update(_category);
			_unitOfWork.Save();
		}
	}
}
