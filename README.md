# Calendar API

## Description
Build an interview calendar API that manages availability time slots between candidates and interviers in order to provide possible slot times where a interview may occour.

## Considerations
- An interview slot is a 1-hour period of time that spreads from the beginning of any hour until the beginning of the next hour. For example, a time span between 9am and 10am is a valid interview slot, whereas between 9:30am and 10:30am is not.
  
- Each of the interviewers sets their availability slots. For example, the interviewer Mary is available next week each day from 9am through 4pm without breaks and the interviewer Diana is available from 12pm to 6pm on Monday and Wednesday next week, and from 9am to 12pm on Tuesday and Thursday.

- Each of the candidates sets their requested slots for the interview. For example, the candidate John is available for the interview from 9am to 10am any weekday next week and from 10am to 12pm on Wednesday.

- Anyone may then query the API to get a collection of periods of time when it’s possible to arrange an interview for a particular candidate and one or more interviewers. In this example, if the API queries for the candidate John and interviewers Mary and Diana, the response should be a collection of 1-hour slots: from 9am to 10am on Tuesday, from 9am to 10am on Thursday.

## TechStack required
- .NET Core 3.1 
- C#

## Additional notes
- There's no need to have a sophisticated persisting mechanism implemented
- You can use public nuget packages
- All API are public, no authentication mechanism required

## Endpoints

### Define person availability

> POST /calendar/availability
```
{
    "name": "{person name}",
    "role": "{person role}"
    "slots": [
        {
            "dateStart": "{start date}",
            "dateEnd": "{end date}"
        },
        {
            "dateStart": "{start date}",
            "dateEnd": "{end date}"
        }
    ]
}
```

### Get availability slots

> GET /calendar/availability/{person c}?interviewers={person a}&interviewers={person b}
```
[
    {
        "dateStart": "{start date}",
        "dateEnd": "{end date}"
    },
    {
        "dateStart": "{start date}",
        "dateEnd": "{end date}"
    }
]
```
