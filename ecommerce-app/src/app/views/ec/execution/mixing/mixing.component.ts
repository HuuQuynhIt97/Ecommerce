import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { Observable, Subject, Subscription } from 'rxjs';
import { IBuilding } from 'src/app/_core/_model/building';
import { IIngredient } from 'src/app/_core/_model/summary';
import { AbnormalService } from 'src/app/_core/_service/abnormal.service';
import { AlertifyService } from 'src/app/_core/_service/alertify.service';
import { IngredientService } from 'src/app/_core/_service/ingredient.service';
import { MakeGlueService } from 'src/app/_core/_service/make-glue.service';
// import * as signalr from '../../../../assets/js/ec-client.js';
// import * as signalr from '../../../../assets/js/weighing-scale-client.js';
import { NgxSpinnerService } from 'ngx-spinner';

import { debounceTime } from 'rxjs/operators';
import { MixingService } from 'src/app/_core/_service/mixing.service';
import { AutoSelectDirective } from '../../select.directive';
import { IMixingDetailForResponse } from 'src/app/_core/_model/IMixingInfo';
import { IScanner } from 'src/app/_core/_model/IToDoList';
import { IMixingInfo } from 'src/app/_core/_model/plan';
import { IRole } from 'src/app/_core/_model/role';
import { SettingService } from 'src/app/_core/_service/setting.service';
import { TodolistService } from 'src/app/_core/_service/todolist.service';
import { environment } from 'src/environments/environment';

const SUMMARY_RECIEVE_SIGNALR = 'ok';
const BIG_MACHINE_UNIT = 'k';
const SMALL_MACHINE_UNIT = 'g';
const BUILDING_LEVEL = 2;
declare var $: any;
const ADMIN = 1;
const SUPERVISOR = 2;
const CONNECTION_WEIGHING_SCALE_HUB = new HubConnectionBuilder()
  .withUrl(environment.scalingHubLocal)
  .withAutomaticReconnect([1000, 3000, 5000, 10000, 30000])
  // .configureLogging(signalR.LogLevel.Information)
  .build();

@Component({
  selector: 'app-mixing',
  templateUrl: './mixing.component.html',
  styleUrls: ['./mixing.component.css'],
  providers: [MixingService, AutoSelectDirective]
})
export class MixingComponent implements OnInit, OnDestroy {
  ingredients: IIngredient[];
  ingredientsTamp: IIngredient;
  building: IBuilding;
  glueID: number;
  disabled = true;
  unit = 'k';
  position: string;
  qrCode: string;
  buildingName: string;
  IsAdmin: true;
  scalingSetting: any;
  buildingID: number;
  scalingKG: string;
  volume: number;
  volumeA: number;
  volumeB: any;
  volumeC: any;
  volumeD: any;
  volumeE: any;
  volumeH: any;
  B: number;
  C: number;
  D: number;
  endTime: Date;
  guidances: IMixingInfo;
  makeGlue: any;
  startTime: any;
  glueName: string;
  role: IRole;
  estimatedTime: any;
  estimatedStartTime: any;
  estimatedFinishTime: any;
  stdcon: number;
  subject = new Subject<IScanner>();
  subscription: Subscription[] = [];
  detail: IMixingDetailForResponse;
  scaleStatus = true;
  checkedSmallScale: boolean;
  tab: string;
  status: boolean = true
  BUIDLING_ID = 0;
  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private ingredientService: IngredientService,
    private makeGlueService: MakeGlueService,
    private abnormalService: AbnormalService,
    private mixingService: MixingService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private settingService: SettingService,
    public todolistService: TodolistService
  ) {
  }
  ngOnDestroy(): void {
    // this.subscription.forEach(item => item.unsubscribe());
    // this.offSignalr();
    this.mixingService.numberOfAttempts = 5;
    this.mixingService.close().then((result) => {
      console.log('Mixing service stopped connection');
    }).catch((err) => {
      console.log('Mixing service can not stopped connection', err);
    });
  }

  ngOnInit() {
    this.mixingService.connect();
    this.checkQRCode();
    this.checkedSmallScale = false;
    const BUIDLING: IBuilding = JSON.parse(localStorage.getItem('building'));
    this.BUIDLING_ID = Number(JSON.parse(localStorage.getItem('buildingID')));
    const ROLE: IRole = JSON.parse(localStorage.getItem('level'));
    this.role = ROLE;
    this.building = BUIDLING;
    this.buildingID = this.BUIDLING_ID;
    this.scalingKG = BIG_MACHINE_UNIT;
    this.startTime = new Date();
    this.getScalingSetting();
    this.onRouteChange();
  }

  onChangeScale(args) {
    this.checkedSmallScale = true;
    this.unit = 'g';
    this.scalingKG = 'g';
  }

  // khi scan qr-code
  async onNgModelChangeScanQRCode(args, item) {
    const scanner: IScanner = {
      QRCode: args,
      ingredient: item
    };
    this.subject.next(scanner);

  }

  private checkQRCode() {
    this.subscription.push(this.subject
      .pipe(debounceTime(500))
      .subscribe(async (arg) => {
        const args = arg.QRCode;
        const item = arg.ingredient;
        this.ingredientsTamp = item;
        this.position = item.position;
        const input = args.split('-') || [];
        const dateAndBatch = /(\d+)-(\w+)-/g;
        const qr = args.match(item.materialNO);
        const validFormat = args.match(dateAndBatch);
        const qrcode = args.replace(validFormat[0], '');
        if (qr === null) {
          this.alertify.warning(`Mã QR không hợp lệ!<br>The QR Code invalid!`);
          this.qrCode = '';
          this.status = false;
          this.errorScan();
          return;
        }
        if (qr !== null) {
          try {
            // check neu batch va code giong nhau
            if (qrcode !== qr[0]) {
              this.alertify.warning(`Mã QR không hợp lệ!<br>Please you should look for the chemical name "${item.name}"`);
              this.qrCode = '';
              this.status = false;
              this.errorScan();
              return;
            }
            this.qrCode = qr[0];
            if (this.qrCode !== item.materialNO) {
              this.alertify.warning(`Mã QR không hợp lệ!<br>Please you should look for the chemical name "${item.name}"`);
              this.qrCode = '';
              this.status = false;
              this.errorScan();
              return;
            }
            if (item.position === 'A') {
              this.stdcon = this.scalingKG === SMALL_MACHINE_UNIT ? this.stdcon * 1000 : this.stdcon;
              this.changeExpected('A', this.stdcon);
              this.checkedSmallScale = true;
              this.startTime = new Date();
            }

            const checkLock = await this.hasLock(
              item.name,
              this.building.name,
              input[1]
            );
            if (checkLock === true) {
              this.alertify.error('Hóa chất này đã bị khóa!<br>This chemical has been locked!');
              this.qrCode = '';
              this.status = false;
              this.errorScan();
              return;
            }

            /// Khi quét qr-code thì chạy signal

            const code = item.code;
            const ingredient = this.findIngredientCode(code);
            this.setBatch(ingredient, input[1]);
            if (ingredient) {
              this.status = true
              this.signal();
              this.changeInfo('success-scan', ingredient.code);
              if (ingredient.expected === 0 && ingredient.position === 'A') {
                this.changeFocusStatus(ingredient.code, false, true);
                this.changeScanStatus(ingredient.code, false);
              } else {
                this.changeScanStatus(ingredient.code, false);
                this.changeFocusStatus(code, false, false);
              }
            }

            // chuyển vị trí quét khi scan

            switch (this.position) {
              case 'A':
                this.changeScanStatusByPosition('A', false);
                this.changeScanStatusByPosition('B', true);
                break;
              case 'B':
                this.changeScanStatusByPosition('B', false);
                this.changeScanStatusByPosition('C', true);
                break;
              case 'C':
                this.changeScanStatusByPosition('C', false);
                // Update by Leo 3/1/2021
                this.changeScanStatusByPosition('D', true);
                break;
              case 'D':
                this.changeScanStatusByPosition('D', false);
                // Update by Leo 3/1/2021
                this.changeScanStatusByPosition('E', true);
                break;
              case 'E':
                // Update by Leo 3/1/2021
                this.changeScanStatusByPosition('H', true);
                break;
            }
          } catch (error) {
            this.errorScan();
            this.alertify.error('Mã QR không hợp lệ!<br>Wrong Chemical!');
            this.qrCode = '';
            return;
          }
        }
      }
      ));
  }

  private signal() {
    this.mixingService.receiveAmount.subscribe(res => {
      const unit = res.unit;
      const scalingMachineID = res.weighingScaleID;
      const message = res.amount;
      if (unit === this.scalingKG) {
        this.volume = parseFloat(message);
        this.unit = unit;
        console.log(this.position);
        switch (this.position) {
          case 'A':
            this.changeActualByPosition('A', this.volume, unit);
            this.checkValidPosition(this.ingredientsTamp, this.volume);
            break;
          case 'B':
            if (this.status) {
              if (unit !== SMALL_MACHINE_UNIT) {
                // update realA
                this.changeActualByPosition('B', this.volume, unit);
                this.checkValidPosition(this.ingredientsTamp, this.volume);
              } else {
                this.changeActualByPosition('B', this.volume, unit);
                this.checkValidPosition(this.ingredientsTamp, this.volume);
              }
              break;
            }
          case 'C':
            if (this.status) {
              this.changeActualByPosition('C', this.volume, unit);
              this.checkValidPosition(this.ingredientsTamp, this.volume);
              break;
            }
          case 'D':
            if (this.status) {
              this.changeActualByPosition('D', this.volume, unit);
              this.checkValidPosition(this.ingredientsTamp, this.volume);
              break;
            }
          case 'E':
            if (this.status) {
              this.changeActualByPosition('E', this.volume, unit);
              this.checkValidPosition(this.ingredientsTamp, this.volume);
              break;
            }
          case 'H':
            if (this.status) {
              this.changeActualByPosition('E', this.volumeH, unit);
              this.checkValidPosition(this.ingredientsTamp, this.volumeH);
              break;
            }
        }
      }
    });
  }

  onRouteChange() {
    this.route.data.subscribe(data => {
      this.glueID = this.route.snapshot.params.glueID;
      this.estimatedStartTime = this.route.snapshot.params.estimatedStartTime;
      this.estimatedFinishTime = this.route.snapshot.params.estimatedFinishTime;
      this.tab = this.route.snapshot.params.tab;
      this.stdcon = +this.route.snapshot.params.stdcon;
      this.getGlueWithIngredientByGlueID();
    });
  }

  getGlueWithIngredientByGlueID() {
    this.spinner.show();
    this.makeGlueService
      .getGlueWithIngredientByGlueID(this.glueID)
      .subscribe((res: any) => {
        this.ingredients = res.ingredients.map((item) => {
          return {
            id: item.id,
            scanStatus: item.position === 'A',
            code: item.code,
            scanCode: '',
            materialNO: item.materialNO,
            name: item.name,
            percentage: item.percentage,
            position: item.position,
            allow: item.allow,
            expected: 0,
            real: 0,
            focusReal: false,
            focusExpected: false,
            valid: false,
            info: '',
            batch: '',
            unit: '',
            time_start: new Date() // leo update 11:13 AM 2/2/2021
          };
        });
        this.glueName = res.name;
        this.getMixingDetail();
        this.spinner.hide();
      }, err => {
        this.spinner.hide();
      });
  }

  // api
  scanQRCode(): Promise<any> {
    return this.ingredientService.scanQRCode(this.qrCode).toPromise();
  }

  // helpers
  private findIngredientCode(code) {
    for (const item of this.ingredients) {
      if (item.code === code) {
        return item;
      }
    }
  }

  private setBatch(item, batch) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].id === item.id) {
        this.ingredients[i].batch = batch;
        break;
      }
    }
  }

  private changeScanStatus(code, scanStatus) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].code === code) {
        this.ingredients[i].scanStatus = scanStatus;
        break; // Stop this loop, we found it!
      }
    }
  }

  private changeFocusStatus(code, focusReal, focusExpected) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].code === code) {
        this.ingredients[i].focusReal = focusReal;
        this.ingredients[i].focusExpected = focusExpected;
        break; // Stop this loop, we found it!
      }
    }
  }

  private changeValidStatus(code, validStatus) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].code === code) {
        this.ingredients[i].valid = validStatus;
        break; // Stop this loop, we found it!
      }
    }
  }

  private changeScanStatusByPosition(position, scanStatus) {
    // this.position = position;
    for (const i in this.ingredients) {
      if (this.ingredients[i].position === position) {
        this.ingredients[i].scanStatus = scanStatus;
        break;
        // Stop this loop, we found it!
      }
    }
  }

  private errorScan() {
    for (const key in this.ingredients) {
      if (this.ingredients[key].scanStatus) {
        const element = this.ingredients[key];
        this.changeInfo('error-scan', element.code);
      }
    }
  }

  private changeInfo(info, code) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].code === code) {
        this.ingredients[i].info = info;
        break; // Stop this loop, found it!
      }
    }
  }

  private changeScanStatusFocus(position, status) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].position === position) {
        this.ingredients[i].scanStatus = status;
        break; // Stop this loop, found it!
      }
    }
  }

  private findIngredient(position) {
    for (const item of this.ingredients) {
      if (item.position === position) {
        return item;
      }
    }
  }

  private calculatorIngredient(weight, percentage) {
    const result = (weight * percentage) / 100;
    return result * 1000 ?? 0;
  }

  private toFixedIfNecessary(value, dp) {
    return +parseFloat(value).toFixed(dp);
  }

  private changeExpectedRange(args, position) {
    const positionArray = ['A', 'B', 'C', 'D', 'E'];
    if (positionArray.includes(position)) {
      const weight = parseFloat(args);
      const expected = this.calculatorIngredient(
        weight,
        this.findIngredient(position)?.percentage
      );
      if (position === 'B') {
        this.B = expected;
      }
      if (position === 'C') {
        this.C = expected;
      }
      if (position === 'D') {
        this.D = expected;
      }
      if (position === 'A') { return; }
      const allow = this.calculatorIngredient(
        expected / 1000,
        this.findIngredient(position)?.allow
      );
      const min = expected - allow;
      const max = expected + allow;
      const minRange = this.toFixedIfNecessary(min / 1000, 3);
      const maxRange = this.toFixedIfNecessary(max / 1000, 3);
      const expectedRange =
        maxRange > 3
          ? `${minRange}kg - ${maxRange}kg`
          : ` ${this.toFixedIfNecessary(min, 1)}g - ${this.toFixedIfNecessary(max, 1)}g `;
      if (allow === 0) {
        const kgValue = this.toFixedIfNecessary(expected / 1000, 3);
        // tslint:disable-next-line:no-shadowed-variable
        const expectedRange = kgValue > 3 ? `${kgValue}kg` : ` ${this.toFixedIfNecessary(kgValue * 1000, 1)}g`;
        this.changeExpected(position, expectedRange);
      } else {
        this.changeExpected(position, expectedRange);
      }
    }
  }

  private changeExpected(position, expected) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].position === position) {
        const expectedResult = expected;
        this.ingredients[i].expected = expectedResult;
        break; // Stop this loop, found it!
      }
    }
  }

  private changeActualByPosition(position, actual, unit) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].position === position) {
        this.ingredients[i].real = actual;
        this.ingredients[i].unit = unit;
        this.ingredients[i].time_start = new Date(); // leo update
        break; // Stop this loop, found it!
      }
    }
  }

  onBlur(data: IIngredient) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].position === data.position) {
        this.ingredients[i].focusReal = true;
      } else {
        this.ingredients[i].focusReal = false;
      }
    }
    this.signal();

  }

  checkValidPosition(ingredient, args) {
    let min;
    let max;
    let minG;
    let maxG;
    const currentValue = ingredient.position === 'A' && this.scalingKG === SMALL_MACHINE_UNIT ? parseFloat(args) / 1000 : parseFloat(args);
    if (ingredient.allow === 0) {
      const unit = ingredient.position === 'A' ? this.stdcon : ingredient.expected.replace(/[0-9|.]+/g, '').trim();
      if (unit !== SMALL_MACHINE_UNIT) {
        min = parseFloat(ingredient.expected);
        max = parseFloat(ingredient.expected);
      } else {
        minG = parseFloat(ingredient.expected);
        maxG = parseFloat(ingredient.expected);
        min = parseFloat(ingredient.expected) / 1000;
        max = parseFloat(ingredient.expected) / 1000;
      }
    } else {
      const exp2 = ingredient.expected.split('-');
      const unit = exp2[0].replace(/[0-9|.]+/g, '').trim();
      if (unit !== SMALL_MACHINE_UNIT) {
        min = parseFloat(exp2[0]);
        max = parseFloat(exp2[1]);
      } else {
        minG = parseFloat(exp2[0]);
        maxG = parseFloat(exp2[1]);
        min = parseFloat(exp2[0]) / 1000;
        max = parseFloat(exp2[1]) / 1000;
      }
    }

    // Nếu Chemical là A, focus vào chemical B
    if (ingredient.position === 'A') {
      const positionArray = ['A', 'B', 'C', 'D', 'E'];
      for (const position of positionArray) {
        this.changeExpectedRange(currentValue, position);
      }
      this.changeScanStatusFocus('A', false);
      this.changeScanStatusFocus('B', true);
      this.changeFocusStatus(ingredient.code, false, false);
      if (this.ingredients.length === 1) {
        this.disabled = false;
      }
    }

    // Nếu Chemical là B, focus vào chemical C
    if (ingredient.position === 'B') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('B', false);
          this.changeScanStatusFocus('C', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 2) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG + 5 && currentValue >= minG) {
          this.changeScanStatusFocus('B', false);
          this.changeScanStatusFocus('C', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 2) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }

    // Nếu Chemical là C, focus vào chemical D
    if (ingredient.position === 'C') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('C', false);
          this.changeScanStatusFocus('D', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 3) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG + 5 && currentValue >= minG) {
          this.changeScanStatusFocus('C', false);
          this.changeScanStatusFocus('D', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 3) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }

    // Nếu Chemical là D, focus vào chemical E
    if (ingredient.position === 'D') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG + 5 && currentValue >= minG) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }

    if (ingredient.position === 'E') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG + 5 && currentValue >= minG) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }
    this.changeReal(ingredient.code, +args);
  }

  private offSignalr() {
    this.mixingService.offWeighingScale();
  }

  private changeReal(code, real) {
    for (const i in this.ingredients) {
      if (this.ingredients[i].code === code) {
        if (this.ingredients[i].position !== 'A') {
        }
        this.ingredients[i].real = this.toFixedIfNecessary(real, 3);
        break; // Stop this loop, found it!
      }
    }
  }

  // event
  showArrow(item): boolean {
    if (item.position === 'A' && item.scanStatus === true) {
      return true;
    }
    if (item.position === 'A' && item.scanStatus === false && item.focusExpected === true) {
      return true;
    }
    if (item.position !== 'A' && item.scanStatus === true) {
      return true;
    }
    return false;
  }

  checkValidPositionForRealEvent(ingredient, data) {
    let min;
    let max;
    let minG;
    let maxG;
    const args = parseFloat(data.target.value);
    const currentValue = args;
    if (ingredient.allow === 0) {
      const pattern = /^[0-9.]+[kg]+\s\+\s[0-9.]+[kg]+|(^[0-9.]+[kg]+)/g;
      const checkFormat = ingredient.expected.toString().match(pattern);
      const unit = checkFormat != null ? ingredient.expected.replace(/[0-9|.]+/g, '').trim() : BIG_MACHINE_UNIT;
      // unit = ingredient.position === 'A' ? BIG_MACHINE_UNIT : unit;
      if (unit !== SMALL_MACHINE_UNIT) {
        min = parseFloat(ingredient.expected);
        max = parseFloat(ingredient.expected);
      } else {
        minG = parseFloat(ingredient.expected);
        maxG = parseFloat(ingredient.expected);
        min = parseFloat(ingredient.expected) / 1000;
        max = parseFloat(ingredient.expected) / 1000;
      }
    } else {
      const exp2 = ingredient.expected.split('-');
      const unit = exp2[0].replace(/[0-9|.]+/g, '').trim();
      if (unit !== SMALL_MACHINE_UNIT) {
        min = parseFloat(exp2[0]);
        max = parseFloat(exp2[1]);
      } else {
        minG = parseFloat(exp2[0]);
        maxG = parseFloat(exp2[1]);
        min = parseFloat(exp2[0]) / 1000;
        max = parseFloat(exp2[1]) / 1000;
      }
    }

    // Nếu Chemical là A, focus vào chemical B
    if (ingredient.position === 'A') {
      const positionArray = ['A', 'B', 'C', 'D', 'E'];
      for (const position of positionArray) {
        ingredient.real = position === 'A' ? currentValue : ingredient.real;
        this.changeExpectedRange(ingredient.real, position);
      }
      this.changeScanStatusFocus('A', false);
      this.changeScanStatusFocus('B', true);
      this.changeFocusStatus(ingredient.code, false, false);
      if (this.ingredients.length === 1) {
        this.disabled = false;
      }
    }

    // Nếu Chemical là B, focus vào chemical C
    if (ingredient.position === 'B') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('B', false);
          this.changeScanStatusFocus('C', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 2) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG && currentValue >= minG) {
          this.changeScanStatusFocus('B', false);
          this.changeScanStatusFocus('C', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 2) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }

    // Nếu Chemical là C, focus vào chemical D
    if (ingredient.position === 'C') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('C', false);
          this.changeScanStatusFocus('D', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 3) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG && currentValue >= minG) {
          this.changeScanStatusFocus('C', false);
          this.changeScanStatusFocus('D', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length === 3) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }

    // Nếu Chemical là D, focus vào chemical E
    if (ingredient.position === 'D') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG && currentValue >= minG) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }

    if (ingredient.position === 'E') {
      if (max > 3) {
        this.scalingKG = BIG_MACHINE_UNIT;
        if (currentValue <= max && currentValue >= min) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      } else {
        this.scalingKG = SMALL_MACHINE_UNIT;
        if (currentValue <= maxG && currentValue >= minG) {
          this.changeScanStatusFocus('D', false);
          this.changeScanStatusFocus('E', true);
          this.changeValidStatus(ingredient.code, false);
          this.changeFocusStatus(ingredient.code, false, false);
          if (this.ingredients.length >= 4) {
            this.disabled = false;
          }
        } else {
          this.disabled = true;
          this.changeFocusStatus(ingredient.code, false, false);
          this.changeValidStatus(ingredient.code, true);
          // this.alertify.warning(`Invalid!`, true);
        }
      }
    }
    this.changeReal(ingredient.code, args);
  }

  realClass(item) {
    const validClass = item.valid === true ? ' warning-focus' : '';
    const className = item.info + validClass;
    return className;
  }

  lockClass(item) {
    return item.scanCode === true ? '' : 'lock';
  }

  onKeyupReal(ingredient, args) {
    if (args.keyCode === 13) {
      this.checkValidPositionForRealEvent(ingredient, args);
    }
  }

  onDblClicked(ingredient, args) {
    const item = this.ingredients.filter(x => x.position === ingredient.position)[0];
    if (item.scanCode !== '') {
      this.ingredients.forEach((part, index, theArray) => {
        this.ingredients[index].scanStatus = false;
      });
      ingredient.focusReal = true;
      this.offSignalr();
    } else {
      this.alertify.warning('Hãy quét mã QR trước!!!', true);
      return;
    }
  }

  onKeyupExpected(item, args) {
    if (args.keyCode === 13) {
      if (item.position === 'A') {
        this.changeExpected('A', args.target.value);
        switch (item.position) {
          case 'A':
            this.changeScanStatusByPosition('B', true);
            break;
          case 'B':
            this.changeScanStatusByPosition('C', true);
            break;
          case 'C':
            this.changeScanStatusByPosition('D', true);
            break;
          case 'D':
            this.changeScanStatusByPosition('E', true);
            break;
        }
        this.resetFocusExpectedAndActual();
      }
    }
  }

  resetFocusExpectedAndActual() {
    let i;
    for (i = 0; i < this.ingredients.length; i++) {
      this.ingredients[i].focusReal = false;
      this.ingredients[i].focusExpected = false;
    }
  }

  // api
  back() {
    this.offSignalr();
    this.router.navigate([
      `/ec/execution/todolist-2/${this.tab}`
    ]);
  }

  private getScalingSetting() {
    this.buildingID = this.BUIDLING_ID;
    this.settingService.getMachineByBuilding(this.buildingID).subscribe((data: any) => {
      this.scalingSetting = data.map(item => item.machineID);
    });
  }

  private getMixingDetail() {
    this.todolistService.getMixingDetail(this.glueName).subscribe(detail => {
      this.detail = detail;
    });
  }

  reloadPage() {
    window.location.reload();
  }

  hasLock(ingredient, building, batch): Promise<any> {
    let buildingName = building;
    if (this.IsAdmin) {
      buildingName = this.buildingName;
    }
    return new Promise((resolve, reject) => {
      this.abnormalService.hasLock(ingredient, buildingName, batch).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => {
          reject(false);
        }
      );
    });
  }

  Finish() {
    if (this.IsAdmin) {
      this.alertify.warning(`Only the workers are able to press "Finished" button!<br> Chỉ có công nhân mới được nhấn "Hoàn Thành!"`, true);
      return;
    }
    this.endTime = new Date();
    const details = this.ingredients.map(item => {
      const amountTemp = item.unit === SMALL_MACHINE_UNIT ? item.real / 1000 : item.real;
      return {
        amount: amountTemp,
        ingredientID: item.id,
        batch: item.batch,
        mixingInfoID: 0,
        position: item.position,
        time_start: item.time_start // Thêm bởi Quỳnh (2/2/2021 11:46)
      };
    });
    const mixing = {
      glueID: this.glueID,
      glueName: this.glueName,
      buildingID: this.BUIDLING_ID,
      mixBy: JSON.parse(localStorage.getItem('user')).user.id,
      estimatedStartTime: this.estimatedStartTime,
      estimatedFinishTime: this.estimatedFinishTime,
      startTime: this.startTime.toISOString(),
      endTime: this.endTime.toISOString(),
      details
    };
    if (mixing) {
      this.makeGlueService.add(mixing).subscribe((glue: any) => {
        this.todolistService.setValue(false);
        this.back();
        this.offSignalr();
        this.alertify.success('The Glue has been finished successfully');
      });
    }
  }

  convertDate(date: Date) {
    const tzoffset = date.getTimezoneOffset() * 60000;
    const localISOTime = (new Date(Date.now() - tzoffset)).toISOString().slice(0, -1);
    return localISOTime;
  }
}
