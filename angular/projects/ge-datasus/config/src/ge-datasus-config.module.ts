import { ModuleWithProviders, NgModule } from '@angular/core';
import { GE_DATASUS_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class GeDatasusConfigModule {
  static forRoot(): ModuleWithProviders<GeDatasusConfigModule> {
    return {
      ngModule: GeDatasusConfigModule,
      providers: [GE_DATASUS_ROUTE_PROVIDERS],
    };
  }
}
