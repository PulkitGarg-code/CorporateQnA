using Microsoft.Extensions.Configuration;
using System.Data;
using SimpleInjector;
using System.Data.SqlClient;
using CorporateQnA.Services.Services;

namespace CorporateQnA.Client
{
	public class SimpleInjectorBootStrap
	{
		public static void InitializeContainer(Container container, IConfiguration configuration)
		{
			container.Register<IDbConnection>(() => new SqlConnection(configuration.GetValue<string>("ConnectionString")), Lifestyle.Singleton);

			container.Register<IQuestionService, QuestionService>(Lifestyle.Singleton);
			container.Register<IReportQuestionService, ReportQuestionService>(Lifestyle.Singleton);
			container.Register<IUserReactionService, UserReactionService>(Lifestyle.Singleton);
			container.Register<ICategoryService, CategoryService>(Lifestyle.Singleton);
			container.Register<IBestAnswerService, BestAnswerService>(Lifestyle.Singleton);
			container.Register<IAnswerService, AnswerService>(Lifestyle.Singleton);
			container.Register<IUserService, UserService>(Lifestyle.Singleton);
			container.Register<IUpVoteService, UpVoteService>(Lifestyle.Singleton);
			container.Register<IAuthService, AuthService>();

		}

	}
}
