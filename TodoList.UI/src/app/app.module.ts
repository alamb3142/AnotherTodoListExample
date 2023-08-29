import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GrpcCoreModule } from '@ngx-grpc/core';
import { GrpcWebClientModule } from '@ngx-grpc/grpc-web-client';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HTTP_API_BASE_URL } from './core/api/http/clients/clients';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
	declarations: [AppComponent],
	imports: [
		BrowserModule,
		AppRoutingModule,
		GrpcCoreModule.forRoot(),
		GrpcWebClientModule.forRoot({
			settings: { host: 'https://localhost:7050' }
		}),
		FontAwesomeModule,
		HttpClientModule
	],
	providers: [
		{ provide: HTTP_API_BASE_URL, useValue: 'http://localhost:5167' },
	],
	bootstrap: [AppComponent]
})
export class AppModule {}
