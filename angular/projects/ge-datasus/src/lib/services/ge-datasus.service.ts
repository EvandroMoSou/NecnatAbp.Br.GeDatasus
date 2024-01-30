import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class GeDatasusService {
  apiName = 'GeDatasus';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/GeDatasus/sample' },
      { apiName: this.apiName }
    );
  }
}
