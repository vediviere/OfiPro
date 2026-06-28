export interface CategoryOption {
  categoryId: string;
  name: string;
}

export interface SubcategoryOption {
  subcategoryId: string;
  categoryId: string;
  name: string;
}