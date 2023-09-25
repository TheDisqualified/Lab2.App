using System;

namespace Lab2.Model;

public enum AirportAdditionError
{
    InvalidIdLength,
    InvalidNumStars,
    InvalidDate,
    DBAdditionError,
    NoError
}
public enum AirportDeletionError
{
    AirportNotFound,
    DBDeletionError,
    NoError
}
public enum AirportEditError
{
    AirportNotFound,
    InvalidFieldError,
    DBEditError,
    NoError
}
