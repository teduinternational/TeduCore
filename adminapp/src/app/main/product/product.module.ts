
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product.component';
import { ProductRouter } from './product.routes';
import { PaginationModule, ModalModule } from 'ngx-bootstrap';
import { FormsModule } from '@angular/forms';
import { DataService } from '@shared/services/data.service';
import { UtilityService } from '@shared/services/utility.service';
import { UploadService } from '@shared/services/upload.service';
import { Daterangepicker } from 'ng2-daterangepicker';
import { SimpleTinyComponent } from '@shared/layouts/simple-tiny/simple-tiny.component';

@NgModule({
  imports: [
        CommonModule,
    ProductRouter,
    FormsModule,
    PaginationModule,
    ModalModule,
    Daterangepicker  ],
   declarations: [ProductComponent,SimpleTinyComponent],
  providers: [DataService, UtilityService, UploadService]
})
export class ProductModule { }
