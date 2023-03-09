using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Service.Base;

/// <summary>
/// 角色
/// </summary>
public class RoleService : BaseService<RoleEntity>, IRoleService
{
    /// <summary>
    /// 
    /// </summary>
    public RoleRepository _roleRepository;
    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="roleRepository"></param>
    public RoleService(RoleRepository roleRepository) : base(roleRepository)
    {
        _roleRepository = roleRepository;
    }
}
