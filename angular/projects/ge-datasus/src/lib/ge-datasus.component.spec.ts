import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { GeDatasusComponent } from './components/ge-datasus.component';
import { GeDatasusService } from '@necnat-abp.Br/ge-datasus';
import { of } from 'rxjs';

describe('GeDatasusComponent', () => {
  let component: GeDatasusComponent;
  let fixture: ComponentFixture<GeDatasusComponent>;
  const mockGeDatasusService = jasmine.createSpyObj('GeDatasusService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [GeDatasusComponent],
      providers: [
        {
          provide: GeDatasusService,
          useValue: mockGeDatasusService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeDatasusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
