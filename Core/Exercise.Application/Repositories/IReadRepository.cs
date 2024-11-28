using Exercise.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Application.Repositories
{
	public interface IReadRepository<T> : IRepository<T> where T : BaseEntitiy
	{
		//tracking mekanizması var. db deki değişiklikleri kontrol eder. burda parametrelerde bunu kontrol ederek hangi methodlarda tracking olacağını kontrol ediyoruz. true verdiğimiz için default olarak tracking kullanılıyor. 
		IQueryable<T> GetAll(bool tracking = true);   //Her şeyi getirir.  
		IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true); //Şarta uyanları getirir.
		Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true	);  //Şarta uyan ilk veriyi getirir.
		Task<T> GetByIdAsync(string id, bool tracking = true); //seçilen veriyi getirir.
	}
}
