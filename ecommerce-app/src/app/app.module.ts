import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy, CommonModule, PathLocationStrategy } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { IconModule, IconSetModule, IconSetService } from '@coreui/icons-angular';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from './app.component';

// Import containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { SlickCarouselModule } from 'ngx-slick-carousel';
const APP_CONTAINERS = [
  DefaultLayoutComponent
];

// import {
//   AppAsideModule,
//   AppBreadcrumbModule,
//   AppHeaderModule,
//   AppFooterModule,
//   AppSidebarModule,
// } from '@coreui/angular';

// Import routing module
import { AppRoutingModule } from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartsModule } from 'ng2-charts';
import { LayoutComponent } from './views/layout/layout/layout.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DropDownListAllModule, MultiSelectModule } from '@syncfusion/ej2-angular-dropdowns';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { GridAllModule } from '@syncfusion/ej2-angular-grids';

import { MomentModule } from 'ngx-moment';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpLoaderFactory } from './views/ec/ec.module';
import { HttpClient } from '@angular/common/http';
import { CoreModule } from './_core/core.module';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { UploaderModule } from '@syncfusion/ej2-angular-inputs';
import { ImageCropperModule } from 'ngx-image-cropper';
import { JwtModule } from '@auth0/angular-jwt';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { SafePipeModule } from 'safe-pipe';
import { BasicAuthInterceptor } from './_core/_helper/basic-auth.interceptor';
import { ErrorInterceptorProvider } from './_core/_service/error.interceptor';
import { AuthGuard } from './_core/_guards/auth.guard';
import { FollowResolver } from './_core/_resolvers/follow.resolvers';
import { GlueResolver } from './_core/_resolvers/glue.resolver';
import { HistoryResolver } from './_core/_resolvers/history.resolvers';
import { ProjectResolver } from './_core/_resolvers/project.resolvers';
import { ProjectDetailResolver } from './_core/_resolvers/projectDetail.resolvers';
import { RoleResolver } from './_core/_resolvers/role.resolvers';
import { TodolistResolver } from './_core/_resolvers/todolist.resolvers';
import { UserResolver } from './_core/_resolvers/user.resolvers';
import { AlertifyService } from './_core/_service/alertify.service';
import { AuthService } from './_core/_service/auth.service';
import {IvyCarouselModule} from 'angular-responsive-carousel';
import { TopNavbarComponent } from './containers/default-layout/top-navbar/top-navbar.component';
import { HeaderUserComponent } from './containers/default-layout/header-user/header-user.component';
import { HomeBannerComponent } from './containers/default-layout/home-banner/home-banner.component';
import { FooterUserComponent } from './containers/default-layout/footer-user/footer-user.component';
import { PolicyUserComponent } from './containers/default-layout/policy-user/policy-user.component';
import { BestSellersComponent } from './containers/default-layout/best-sellers/best-sellers.component';
import { AdsOneComponent } from './containers/default-layout/ads-one/ads-one.component';
import { AdsTwoComponent } from './containers/default-layout/ads-two/ads-two.component';
import { AdsThreeComponent } from './containers/default-layout/ads-three/ads-three.component';
import { SectionFeaturedComponent } from './containers/default-layout/section-featured/section-featured.component';
import { BestSellingComponent } from './containers/default-layout/best-selling/best-selling.component';
import { ClassifiedAdsComponent } from './containers/default-layout/classified-ads/classified-ads.component';
import { HomeCategoriesComponent } from './containers/default-layout/home-categories/home-categories.component';
import { TopCategoriesComponent } from './containers/default-layout/top-categories/top-categories.component';
import { FlashSaleComponent } from './containers/default-layout/flash-sale/flash-sale.component';
export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  imports: [
    IvyCarouselModule,
    SlickCarouselModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    DropDownListAllModule,
    MultiSelectModule,
    HttpClientModule,
    SafePipeModule,
    GridAllModule,
    MomentModule,
    InfiniteScrollModule,
    ImageCropperModule,
    UploaderModule,
    CoreModule,
    NgxSpinnerModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      defaultLanguage: 'vi'
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter,
      }
    }),
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    // AppAsideModule,
    // AppBreadcrumbModule.forRoot(),
    // AppFooterModule,
    // AppHeaderModule,
    // AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    IconModule,
    IconSetModule.forRoot(),
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    P404Component,
    P500Component,
    LoginComponent,
    LayoutComponent,
    RegisterComponent,
    TopNavbarComponent,
    HeaderUserComponent,
    HomeBannerComponent,
    FooterUserComponent,
    PolicyUserComponent,
    BestSellersComponent,
    AdsOneComponent,
    AdsTwoComponent,
    AdsThreeComponent,
    SectionFeaturedComponent,
    BestSellingComponent,
    ClassifiedAdsComponent,
    HomeCategoriesComponent,
    TopCategoriesComponent,
    FlashSaleComponent,
  ],
  providers: [
    AuthGuard,
    AlertifyService,
    AuthGuard,
    NgxSpinnerService,
    ErrorInterceptorProvider,
    ProjectResolver,
    TodolistResolver,
    HistoryResolver,
    FollowResolver,
    UserResolver,
    ProjectDetailResolver,
    RoleResolver,
    AuthService,
    GlueResolver,
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    },
    IconSetService,
    { provide: HTTP_INTERCEPTORS, useClass: BasicAuthInterceptor, multi: true },
    // Location,
    // {provide: LocationStrategy, useClass: PathLocationStrategy}
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
