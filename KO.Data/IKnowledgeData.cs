
using KO.Core;
using KO.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KO.Data
{
    public interface IKnowledgeData
    {
        IEnumerable<FieldText> GetAll(string optionalstr = "");

        FieldText GetById(int id);

        FieldText Update(FieldText updatedFieldText);

        FieldText Add(FieldText newFieldText);

        FieldText Delete(int id);
        int GetCountOfFieldTexts();
        int Commit();
    }

    public class SqlKnowledgeData : IKnowledgeData
    {
        private readonly KnowledgeDbContext db;
        public SqlKnowledgeData(KnowledgeDbContext dbContext)
        {
            db = dbContext;
        }


        public int GetCountOfFieldTexts()
        {
            return db.FieldText.Count();
        }

        public FieldText Add(FieldText newFieldText)
        {
            db.Add(newFieldText);
            return newFieldText;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public FieldText Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                db.FieldText.Remove(item);
            }
            return item;
        }

        public IEnumerable<FieldText> GetAll(string name)
        {
          
                var query = from item in db.FieldText
                            where item.Text.StartsWith(name)
                            || string.IsNullOrEmpty(name)
                            orderby item.Text
                            select item;

            return query;
        }

        public FieldText GetById(int id)
        {
            var item = db.FieldText.Find(id);
            return item;
        }

        public FieldText Update(FieldText updatedFieldText)
        {
            var entity = db.FieldText.Attach(updatedFieldText);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return updatedFieldText;

        }
    }

    public class InMemoryIKnowledgeData : IKnowledgeData
    {
       readonly List<FieldText> FieldTexts;
        public InMemoryIKnowledgeData()
        {
            FieldTexts = new List<FieldText>()
            {
                new FieldText { Id=1,FieldType=FieldType.XAMARIN,Text="Bonjour",RelatedMaterialsLinks = "Morning",Keywords="Morning",Usecases = "In the morning"},
                new FieldText { Id=2,FieldType=FieldType.ASP_NET_CORE,Text="Kaaro",RelatedMaterialsLinks = "Morning",Keywords="Kaaro",Usecases = "Ekaaro"},
            };
        }

        public IEnumerable<FieldText> GetAll(string name)
        {
            return from l in FieldTexts
                   orderby l.Text
                   select l;           
        }

        public FieldText GetById(int id)
        {
           //return FieldTexts.Find(x => x.Id == id);

            return FieldTexts.SingleOrDefault(x => x.Id == id);
        }

        public FieldText Update(FieldText updatedFieldText)
        {
            var FieldText = FieldTexts.SingleOrDefault(x => x.Id == updatedFieldText.Id);
            if (FieldText != null)
            {


            }

            return FieldText;
        }

        public FieldText Add(FieldText newFieldText)
        {
            FieldTexts.Add(newFieldText);
            newFieldText.Id = FieldTexts.Max(l => l.Id) + 1;
            return newFieldText;
        }

        //transactional sake
        public int Commit()
        {
            return 0;
        }

        public int GetCountOfFieldTexts()
        {
            return FieldTexts.Count();
        }

        public FieldText Delete(int id)
        {
            var item = FieldTexts.FirstOrDefault(x => x.Id == id);
            if (item!=null)
            {
                FieldTexts.Remove(item);
            }
            return item;
        }
    }

}
