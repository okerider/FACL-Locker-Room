# FACL-Locker-Room

## Asp.Net Web API project.

## GET: GetCurrentVersion
Return the current app version number
that should be stored in the appsettings.json config > AppVersion

## POST: CreateAccount
Receive first and last name, gender, date of birth and nationality.
- Store the received payload to a json file with the following name
format: /accounts/[first-name]-[last-name].json
- If the file name already exist, return an HttpResponse code 400
with message: Account already exist
- Else, return a 200 HttpResponse with message: Account created

## GET: GetAccount
(Parameter: first-name and last-name)
- Check the /account/ folder for any matching account
- Return the data contained in the file, matching the payload that
was saved
