using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Service.Base;

/// <summary>
/// 页面权限
/// </summary>
public class RolesUserPermissionService : BaseService<RolesUserPermissionEntity>, IRolesUserPermissionService
{
    /// <summary>
    /// 
    /// </summary>
    public RolesUserPermissionRepository _rolesUserPermissionRepository;
    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="rolesUserPermissionRepository"></param>
    public RolesUserPermissionService(RolesUserPermissionRepository rolesUserPermissionRepository) : base(rolesUserPermissionRepository)
    {
        _rolesUserPermissionRepository = rolesUserPermissionRepository;
    }
}
