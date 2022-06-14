using Moq;
using Moq.EntityFrameworkCore;
using PackingSlipApi.Controllers;
using PackingSlipApi.Models;
using System;
using System.Collections.Generic;

namespace PackingSlipApiTests
{
    public abstract class BehaviorBase
    {
        readonly protected Mock<PhoenixContext> PhoenixContext = new Mock<PhoenixContext>();
        readonly protected List<OrderHeader> OrderHeaders = new List<OrderHeader>();
        readonly protected List<OrderDetail> OrderDetails = new List<OrderDetail>();
        readonly protected PackingSlipController PackingSlipController;

        protected BehaviorBase()
        {
            PhoenixContext.Setup(m => m.OrderHeaders).ReturnsDbSet(OrderHeaders);
            PhoenixContext.Setup(m => m.OrderDetails).ReturnsDbSet(OrderDetails);

            //var packingSlipService = new PackingSlipService(PhoenixContext.Object);
            //PackingSlipController = new PackingSlipController(packingSlipService);

            //PackingSlipController = new PackingSlipController();
        }
    }
}
