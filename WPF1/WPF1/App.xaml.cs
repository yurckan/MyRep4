﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF1
{
    public partial class App : Application
    {
        public static readonly TkanEntities Context = new TkanEntities();

    }
}
