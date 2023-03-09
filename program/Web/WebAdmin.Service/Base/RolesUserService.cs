using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Service.Base;

/// <summary>
/// 用户角色对应关系
/// </summary>
public class RolesUserService : BaseService<RolesUserEntity>, IRolesUserService
{
    /// <summary>
    /// 
    /// </summary>
    public RolesUserRepository _rolesUserRepository;
    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="rolesUserRepository"></param>
    public RolesUserService(RolesUserRepository rolesUserRepository) : base(rolesUserRepository)
    {
        _rolesUserRepository = rolesUserRepository;
    }
}
