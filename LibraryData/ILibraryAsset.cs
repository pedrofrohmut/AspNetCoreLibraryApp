﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Models;

namespace LibraryData
{
  public interface ILibraryAsset
  {
    IEnumerable<LibraryAsset> GetAll();

    LibraryAsset GetById(int id);

    void Add(LibraryAsset newAsset);

    string GetAuthorOrDirector(int id);

    string GetDeweyIndex(int id);

    string GetType(int id);

    string GetTitle(int id);

    string GetIsbn(int id);

    LibraryBranch GetCurrentLocation(int id);
  }
}
