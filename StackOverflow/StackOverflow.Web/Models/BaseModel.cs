using Autofac;
using AutoMapper;

namespace StackOverflow.Web.Models;

public abstract class BaseModel
{
   
    protected ILifetimeScope _scope = null!;

    public BaseModel()
    {

    }

    public BaseModel(ILifetimeScope scope)
    {
       
        _scope = scope;
    }

    public virtual void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        
    }
}
