import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileManageComponent } from './file-manage/file-manage.component';
import { AppCommonModule } from '../app-common/app-common.module';
import { RouterModule } from '@angular/router';
import { LoginGuard } from 'src/app/core/guards/login.guard';
import { FileIconComponent } from './file-icon/file-icon.component';
import { NzUploadModule, NzAlertModule } from 'ng-zorro-antd';
import { ColumnManageComponent } from './column-manage/column-manage.component';
import { ArticleManageComponent } from './article-manage/article-manage.component';
import { ArticleEditComponent } from './article-edit/article-edit.component';
import { EditorModule, TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular';

@NgModule({
  declarations: [
    FileManageComponent,
    FileIconComponent,
    ColumnManageComponent,
    ArticleManageComponent,
    ArticleEditComponent
  ],
  imports: [
    CommonModule,
    AppCommonModule,
    NzUploadModule,
    RouterModule.forChild([
      { path: 'file', component: FileManageComponent, canActivate: [LoginGuard] },
      { path: 'column', component: ColumnManageComponent, canActivate: [LoginGuard] },
      { path: 'article', component: ArticleManageComponent, canActivate: [LoginGuard] },
      { path: 'article/edit', component: ArticleEditComponent, canActivate: [LoginGuard] },

    ]),
    EditorModule,
    NzAlertModule 
  ],
  providers: [
    { provide: TINYMCE_SCRIPT_SRC, useValue: 'tinymce/tinymce.min.js' }
  ]
})
export class ContentManageModule { }
