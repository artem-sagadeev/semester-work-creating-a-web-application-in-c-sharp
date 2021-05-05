using System.Linq;
using System.Threading.Tasks;
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
            if (RequiredSubscriptionType == PriceType.Free)
                return true;

            if (user is null)
                return false;

            var isUserAuthorOfPost = user.UserId == UserId;
            var hasSubscribeToUser = await subscriptionService.HasUserAccess(user.UserId,
                UserId,
                RequiredSubscriptionType,
                TypeOfSubscription.User);

            if (ProjectId == 0)
                return false;
            
            var isUserMemberOfProject = (await developerService.GetProjectUsers(ProjectId))
                .Select(u => u.Id)
                .Contains(user.UserId);
            var hasSubscribeToProject =  await subscriptionService.HasUserAccess(user.UserId,
                                            ProjectId,
                                            RequiredSubscriptionType,
                                            TypeOfSubscription.Project);

            
            var companyId = (await developerService.GetProjectCompany(ProjectId)).Id;

            if (companyId == 0)
                return false;
            
            var isUserMemberOfCompany = (await developerService.GetCompanyUsers(companyId))
                .Select(u => u.Id)
                .Contains(user.UserId);
            var hasSubscribeToCompany = await subscriptionService.HasUserAccess(user.UserId, 
                                            companyId, 
                                            RequiredSubscriptionType, 
                                            TypeOfSubscription.Team);
            
            return isUserAuthorOfPost ||
                   hasSubscribeToUser || 
                   isUserMemberOfProject ||
                   hasSubscribeToProject || 
                   isUserMemberOfCompany ||
                   hasSubscribeToCompany;
        }
    }
}