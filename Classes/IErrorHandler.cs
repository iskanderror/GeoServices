using System;

namespace GeoServices.Classes
{
  public interface IErrorHandler
  {
    void HandleError(Exception ex);
  }
}
