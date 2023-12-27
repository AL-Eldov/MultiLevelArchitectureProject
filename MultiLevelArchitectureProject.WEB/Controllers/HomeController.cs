using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiLevelArchitectureProject.BLL.DTO;
using MultiLevelArchitectureProject.BLL.Interfaces;
using MultiLevelArchitectureProject.WEB.Models;

namespace MultiLevelArchitectureProject.WEB.Controllers;

public class HomeController : Controller
{
    IApplicationService applicationService;
    public HomeController(IApplicationService serv)
    {
        applicationService = serv;
    }
    public IActionResult ShowUsers()
    {
        IEnumerable<UserDTO> userDtos = applicationService.GetUsers();
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
        var users = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDtos);
        return View(users);
    }
    public IActionResult CreateUser()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CreateUser(UserViewModel user)
    {
        applicationService.AddUser(new UserDTO { Id = user.Id, FullName = user.FullName, BornDate = user.BornDate });
        return RedirectToAction("ShowUsers");
    }
    public IActionResult DeleteUser(int? id)
    {
        if (id != null)
        {
            applicationService.RemoveUser(id.Value);
            return RedirectToAction("ShowUsers");
        }
        return NotFound();
    }
    public IActionResult EditUser(int? id)
    {
        if (id != null)
        {
            UserDTO userDTO = applicationService.GetUsers().ToList().FirstOrDefault(p => p.Id == id)!;
            UserViewModel? userViewModel = new UserViewModel { Id = userDTO.Id, FullName = userDTO.FullName, BornDate = userDTO.BornDate };
            if (userViewModel != null)
                return View(userViewModel);
        }
        return NotFound();
    }
    [HttpPost]
    public IActionResult EditUser(UserViewModel user)
    {
        UserDTO? userDTO = new UserDTO { Id = user.Id, FullName = user.FullName, BornDate = user.BornDate };
        applicationService.UpdateUser(userDTO);
        return RedirectToAction("ShowUsers");
    }
    public IActionResult ShowPurchases()
    {
        IEnumerable<PurchaseDTO> purchaseDTOs = applicationService.GetPurchases();
        List<PurchaseViewModel> purchases = new List<PurchaseViewModel>();
        foreach (var purchaseDTO in purchaseDTOs)
        {
            if (purchaseDTO.user is not null)
            {
                Console.WriteLine("i am here");

                UserViewModel userViewModel = new UserViewModel { Id = purchaseDTO.user.Id, FullName = purchaseDTO.user.FullName, BornDate = purchaseDTO.user.BornDate };
                purchases.Add(new PurchaseViewModel { Id = purchaseDTO.Id, userId = purchaseDTO.userId, price = purchaseDTO.price, date = purchaseDTO.date, user = userViewModel });
            }
        }
        return View(purchases);
    }
    public IActionResult CreatePurchase()
    {

        ViewBag.userIds = applicationService.GetUsers().Select(u => u.Id).ToList();
        return View();
    }
    [HttpPost]
    public IActionResult CreatePurchase(PurchaseViewModel purchase)
    {
        applicationService.AddPurchase(new PurchaseDTO { Id = purchase.Id, userId = purchase.userId, price = purchase.price, date = purchase.date });
        return RedirectToAction("ShowPurchases");

    }
    public IActionResult DeletePurchase(int? id)
    {
        if (id != null)
        {
            applicationService.RemovePurchase(id.Value);
            return RedirectToAction("ShowPurchases");
        }
        return NotFound();
    }
    public IActionResult EditPurchase(int? id)
    {
        if (id != null)
        {
            PurchaseDTO? purchaseDTO = applicationService.GetPurchases().ToList().FirstOrDefault(p => p.Id == id)!;
            if (purchaseDTO != null)
            {
                PurchaseViewModel? purchaseViewModel = new PurchaseViewModel { Id = purchaseDTO.Id, userId = purchaseDTO.userId, price = purchaseDTO.price, date = purchaseDTO.date };
                ViewBag.userIds = applicationService.GetUsers().Select(u => u.Id).ToList();
                return View(purchaseViewModel);
            }
        }
        return NotFound();
    }
    [HttpPost]
    public IActionResult EditPurchase(PurchaseViewModel purchase)
    {
        PurchaseDTO? purchaseDTO = new PurchaseDTO { Id = purchase.Id, userId = purchase.userId, price = purchase.price, date = purchase.date };
        applicationService.UpdatePurchase(purchaseDTO);
        return RedirectToAction("ShowPurchases");
    }
    protected override void Dispose(bool disposing)
    {
        applicationService.Dispose();
        base.Dispose(disposing);
    }
}
