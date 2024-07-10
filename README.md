<a asp-action="Create">Create New</a>

<form asp-action="SearchByStatus" method="get">
    <div>
        <label for="status">Search Students by Status:</label>
        <select id="status" name="status">
            <option value="">Select Status</option>
            <option value="Active">Active</option>
            <option value="Inactive">Inactive</option>
        </select>
        <button type="submit">Search</button>
    </div>
</form>

<form asp-action="SortByStatus" method="get">
    <div>
        <label for="sortOrder">Sort Students by Status:</label>
        <select id="sortOrder" name="sortOrder">
            <option value="">Select Sort Order</option>
            <option value="ActiveFirst">Active First</option>
            <option value="InactiveFirst">Inactive First</option>
        </select>
        <button type="submit">Sort</button>
    </div>
</form>

<hr>
[HttpGet]
        public async Task<IActionResult> SearchByStatus(string status)
        {
            var students = await _context.Students
                                        .Where(s => s.Status == status)
                                        .ToListAsync();
            return View("Index", students);
        }


        [HttpGet]
        public async Task<IActionResult> SortByStatus(string sortOrder)
        {
            IQueryable<Student> studentsQuery = _context.Students;

            if (sortOrder == "ActiveFirst")
            {
                studentsQuery = studentsQuery.OrderBy(s => s.Status == "Inactive").ThenBy(s => s.Status);
            }
            else if (sortOrder == "InactiveFirst")
            {
                studentsQuery = studentsQuery.OrderBy(s => s.Status == "Active").ThenBy(s => s.Status);
            }
            else
            {
                studentsQuery = studentsQuery.OrderBy(s => s.Status);
            }

            var students = await studentsQuery.ToListAsync();

            return View("Index", students);
        }
