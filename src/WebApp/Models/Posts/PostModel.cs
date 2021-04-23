using System.Threading.Tasks;
using WebApp.Models.Developer;
using WebApp.Models.Identity;
using WebApp.Models.Subscription;
using WebApp.Services.Developer;
using WebApp.Services.Subscription;

namespace WebApp.Models.Posts
{
    public class PostModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Text { get; set; }
        public PriceType RequiredSubscriptionType { get; set; }

        public async Task<bool> HasUserAccessAsync(ApplicationUser user, 
            ISubscriptionService subscriptionService, 
            IDeveloperService developerService)
        {
            var hasSubscribeToUser = user != null &&
                                     await subscriptionService.HasUserAccess(user.UserId, 
                                         UserId, 
                                         RequiredSubscriptionType, 
                                         TypeOfSubscription.User);
            
            var hasSubscribeToProject = ProjectId != 0 && user != null && 
                                        await subscriptionService.HasUserAccess(user.UserId, 
                                            ProjectId,
                                            RequiredSubscriptionType, 
                                            TypeOfSubscription.Project);

            var companyId = ProjectId != 0 ? (await developerService.GetProjectCompany(ProjectId)).Id : 0;
            var hasSubscribeToCompany = companyId != 0 && user != null && 
                                        await subscriptionService.HasUserAccess(user.UserId, 
                                            companyId, 
                                            RequiredSubscriptionType, 
                                            TypeOfSubscription.Team);
            
            return RequiredSubscriptionType == PriceType.Free || 
                   hasSubscribeToUser || 
                   hasSubscribeToProject || 
                   hasSubscribeToCompany;
        }
    }
}