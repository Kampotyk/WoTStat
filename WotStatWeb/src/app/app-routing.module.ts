import { Routes, RouterModule } from '@angular/router'
import { NgModule } from '@angular/core'
import { StatComponent } from './stat/stat.component'
import { HomeComponent } from './home/home.component'
import { AboutComponent } from './about/about.component'

const appRoutes: Routes = [
    {
        path: 'stat',
        component: StatComponent
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'about',
        component: AboutComponent
    },
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    }
]

@NgModule({
    imports: [
        RouterModule.forRoot(
            appRoutes
        )
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingmodule { }
