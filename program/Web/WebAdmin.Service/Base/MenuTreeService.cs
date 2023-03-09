using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Repositories.Base;

namespace WebAdmin.Service.Base;

/// <summary>
/// 页面菜单
/// </summary>
public class MenuTreeService : BaseService<MenuTreeEntity>, IMenuTreeService
{
    /// <summary>
    /// 
    /// </summary>
    public MenuTreeRepository _menuTreeRepository;
    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="menuTreeRepository"></param>
    public MenuTreeService(MenuTreeRepository menuTreeRepository) : base(menuTreeRepository)
    {
        _menuTreeRepository = menuTreeRepository;
    }
}
