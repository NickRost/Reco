import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TimeService {

  constructor() { }

  public calculateTimeDifference(oldDate: Date) {

    const SECOND = 1;
    const MINUTE = 60 * SECOND;
    const HOUR = 60 * MINUTE;
    const DAY = 24 * HOUR;
    const MONTH = 30 * DAY;
    let timeSpan = (new Date().getTime() - new Date(oldDate).getTime())/1000;
    let delta = Math.floor(timeSpan);

    if (delta < 1 * MINUTE)
      return timeSpan == 1 ? 'one second ago' : Math.floor(timeSpan) + ' seconds ago';

    if (delta < 2 * MINUTE)
      return 'a minute ago';

    if (delta < 45 * MINUTE)
      return Math.floor(timeSpan / MINUTE) + ' minutes ago';

    if (delta < 90 * MINUTE)
      return 'an hour ago';

    if (delta < 24 * HOUR)
      return Math.floor(timeSpan / HOUR) + ' hours ago';

    if (delta < 48 * HOUR)
      return 'yesterday';

    if (delta < 30 * DAY)
      return Math.floor(timeSpan / DAY) + ' days ago';

    if (delta < 12 * MONTH)
    {
      let months = Math.floor(timeSpan / DAY / 30);
      return months <= 1 ? 'one month ago' : months + ' months ago';
    }
    else
    {
      let years = Math.floor(timeSpan / DAY / 365);
      return years <= 1 ? 'one year ago' : years + ' years ago';
    }

  }
}
