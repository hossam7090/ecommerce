using bulky.Models.Models;
using Bullky.DataAccess.Repository.IRepository;

namespace bulky.Areas.Admin.Controllers.Command
{
	public class CreateCategoryCommand : ICommand
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly Category _category;

		public CreateCategoryCommand(IUnitOfWork unitOfWork, Category category)
		{
			_unitOfWork = unitOfWork;
			_category = category;
		}

		public void Execute()
		{
			_unitOfWork.Category.Add(_category);
			_unitOfWork.Save();
		}
	}
}
