﻿using Gimji.Data;
using Gimji.DTO.Request.Category;
using Gimji.Models;
using Gimji.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gimji.Services.Implementations
{
    public class CategoryService : CategoryRepository
    {
        private MyPostgresDbContext dbContext;
        public CategoryService(MyPostgresDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public async Task AddCateogory(addCategoryCode categoryCode)
        {
            CategoryCode newCategory = new CategoryCode();
            newCategory.Name = categoryCode.Name;
            newCategory.Description = categoryCode.Description;
            newCategory.Image = categoryCode.Image;
            await dbContext.categoryCodes.AddAsync(newCategory);

            await dbContext.SaveChangesAsync();
           
        }

        public async Task DeleteCategory(string id)
        {
            dbContext.categoryCodes.Remove(await dbContext.categoryCodes.FindAsync(id));
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesItemExist(string id)
        {
            return await dbContext.categoryCodes.FindAsync(id) != null;
        }

        public async Task<IEnumerable<CategoryCode>> GetAllCategory()
        {
            return await dbContext.categoryCodes.ToListAsync();
        }

        public async Task<CategoryCode> GetCategoryById(string id)
        {
            return await dbContext.categoryCodes.SingleOrDefaultAsync(item => item.CodeValue == id);
        }

        public async Task<CategoryCode> GetCategoryByName(string name)
        {
            return await dbContext.categoryCodes.SingleOrDefaultAsync(item => item.Name == name);
        }

        public async Task UpdateCategory(string CodeValue,updateCategoryCode updateCategoryCode)
        {
            var category_Update = await dbContext.categoryCodes.FindAsync(CodeValue);

            if(category_Update != null)
            {
                category_Update.Name = updateCategoryCode.Name;
                category_Update.Image = updateCategoryCode.Image;
                category_Update.Description = updateCategoryCode.Description;

                await dbContext.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu
            }
            else
            {
                throw new Exception("khong tim danh muc can cap nhat");
            }


        }
            
    }
}
