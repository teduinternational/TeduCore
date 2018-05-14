import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductCategoryComponent } from './product-category.component';
import { ProductCategoryRouter } from './product-category.routes';

import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap';
import { DataService } from '@shared/services/data.service';
import { UtilityService } from '@shared/services/utility.service';

@NgModule({
  imports: [
    CommonModule,
    ProductCategoryRouter,
    ModalModule,
    FormsModule
  ],
  declarations: [ProductCategoryComponent],
  providers: [DataService, UtilityService]
})
export class ProductCategoryModule { }
