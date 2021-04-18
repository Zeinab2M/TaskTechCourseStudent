using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTech_ApplicationCore.Interface
{
    public interface IStudentCourse<TEntiy>
    {
        IList<TEntiy> List();
        void Add(TEntiy entiy);
        TEntiy Find(int Id);
        void Update( TEntiy entity);
        void Delete(int Id);
        List<TEntiy> Search(string term);
        List<TEntiy> Search(int id);
    }
}
