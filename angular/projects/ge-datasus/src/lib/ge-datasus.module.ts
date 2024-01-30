import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { GeDatasusComponent } from './components/ge-datasus.component';
import { GeDatasusRoutingModule } from './ge-datasus-routing.module';

@NgModule({
  declarations: [GeDatasusComponent],
  imports: [CoreModule, ThemeSharedModule, GeDatasusRoutingModule],
  exports: [GeDatasusComponent],
})
export class GeDatasusModule {
  static forChild(): ModuleWithProviders<GeDatasusModule> {
    return {
      ngModule: GeDatasusModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<GeDatasusModule> {
    return new LazyModuleFactory(GeDatasusModule.forChild());
  }
}
