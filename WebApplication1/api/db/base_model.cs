using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.helping;

namespace db;

public abstract class base_model
{
    [attributes.PositiveInteger]
    public int id
    {
        get;
        set;
    }
}