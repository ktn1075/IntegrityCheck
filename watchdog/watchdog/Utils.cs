using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


 class Utils
 {
    private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
  
    public static void WithExceptionHandled(string name, bool propagate, Action fn)
    {
        _logger.Info("{:l}", name);

        try
        {
            fn();
        }
        catch (Exception e)
        {
            _logger.Error(e, "{} fail.", name);

            if (propagate)
                throw;
        }
    }
}
