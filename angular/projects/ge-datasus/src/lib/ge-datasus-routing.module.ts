import { NgModule } from '@angular/core';
import { RouterOutletComponent } from '@abp/ng.core';
import { Routes, RouterModule } from '@angular/router';
import { GeDatasusComponent } from './components/ge-datasus.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: RouterOutletComponent,
    children: [
      {
        path: '',
        component: GeDatasusComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GeDatasusRoutingModule {}
